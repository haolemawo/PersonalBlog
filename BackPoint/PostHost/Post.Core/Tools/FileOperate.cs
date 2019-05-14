using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Post.Core.Tools
{
    public class FileOperate
    {
        /// <summary>
        /// 将文章存储到硬盘上
        /// 2019/5/8
        /// </summary>
        /// <param name="content">文章内容</param>
        /// <param name="articleTitle">文章标题</param>
        /// <returns>文章路径</returns>
        public static async Task<string> StoreToTxtFileAsync(string content, string articleTitle, ILogger _logger)
        {
            //因为要存储到硬盘，所以ArticleTitle中不允许出现斜杠/
            articleTitle = articleTitle.Replace("/", "_");
            articleTitle = articleTitle.Replace(@"\", "_");
            articleTitle = articleTitle.Replace("|", "_");

            var waitAndRetryPolly = Policy.Handle<IOException>()
                .WaitAndRetryAsync(new[] {
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(15)
                }, (exception, timeSpan, context) =>
                 {
                     //记录异常
                     _logger.LogError(exception, exception.Message);
                 });

            string redirectPath = articleTitle + "_" + DateTime.Now.ToFileTime() + ".txt";
            string completedPath = Path.Combine(PostConsts.BaseTrail, redirectPath);

            var result = await waitAndRetryPolly.ExecuteAsync(async() => await StoreToTxtAsync(completedPath, content, _logger));
            if (result)
            {
                //如果存储成功，返回存储的路径
                return completedPath;
            }
            else {
                //如果存储不成功，删除已经创建的文件，并返回""
                await waitAndRetryPolly.ExecuteAsync(() => DeleteTxtAsync(completedPath, _logger));
                return "";
            }
        }

        /// <summary>
        /// 更新现有文件
        /// 2019/5/12
        /// </summary>
        /// <param name="completePath">文件绝对路径</param>
        /// <param name="articleContent">文件内容</param>
        /// <returns></returns>
        public static async Task<bool> UpdateArticleContentAsync(string completePath, string articleContent,ILogger _logger)
        {
            var waitAndRetryPolly = Policy.Handle<IOException>()
                .WaitAndRetryAsync(new[] {
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(10),
                            TimeSpan.FromSeconds(15)
                }, (exception, timeSpan, context) =>
                {
                    //记录异常
                    _logger.LogError(exception, exception.Message);
                });
            return await waitAndRetryPolly.ExecuteAsync(async () => await StoreToTxtAsync(completePath, articleContent, _logger));
        }

        /// <summary>
        /// 根据文章的路径来获取文章内容
        /// 2019/5/9
        /// </summary>
        /// <param name="articlePath">文章路径</param>
        /// <returns>文章内容</returns>
        public static async Task<string> GetContentByPathAsync(string articlePath, ILogger _logger)
        {
            var waitAndRetryPolly = Policy.Handle<IOException>()
                .WaitAndRetryAsync(new[] {
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(10),
                                TimeSpan.FromSeconds(15)
                }, (exception, timeSpan, context) =>
                {
                    //记录异常
                    _logger.LogError(exception, exception.Message);
                });
            return await waitAndRetryPolly.ExecuteAsync(async () => await GetDetailContentAsync(articlePath, _logger));
        }

        /// <summary>
        /// 删除硬盘上存储的文章
        /// 2019/5/13
        /// </summary>
        /// <param name="completedPath">文章路径</param>
        /// <returns></returns>
        public static async Task DeleteArticleAsync(string completedPath, ILogger _logger)
        {
            var waitAndRetryPolly = Policy.Handle<IOException>()
                .WaitAndRetryAsync(new[] {
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(10),
                                TimeSpan.FromSeconds(15)
                }, (exception, timeSpan, context) =>
                {
                    //记录异常
                    _logger.LogError(exception, exception.Message);
                });
            await waitAndRetryPolly.ExecuteAsync(() => DeleteTxtAsync(completedPath, _logger));
        }

        /// <summary>
        /// 存储文章到Txt
        /// 2019/5/8
        /// </summary>
        /// <param name="completedPath">完整路径</param>
        /// <param name="content">文章内容</param>
        /// <returns></returns>
        private static async Task<bool> StoreToTxtAsync(string completedPath, string content,ILogger _logger)
        {
            try
            {
                using (FileStream fs = new FileStream(path: completedPath, FileMode.Create))
                {
                    //需要支持中文
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                    {
                        await sw.WriteAsync(content);
                    }
                }
                return true;
            }
            catch (Exception ex){
                //做异常记录
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 删除写入失败的文件
        /// 2019/5/8
        /// </summary>
        /// <param name="completedPath">最终的完整路径</param>
        private static async Task DeleteTxtAsync(string completedPath, ILogger _logger)
        {
            await Task.Run(() =>
            {
                try
                {
                    File.Delete(completedPath);
                }
                catch (Exception ex)
                {
                    //做异常记录
                    _logger.LogError(ex, ex.Message);
                }
            });
        }

        /// <summary>
        /// 根据文章路径读取文章内容
        /// 2019/5/9
        /// </summary>
        /// <param name="articlePath">文章路径</param>
        /// <returns>文章内容</returns>
        private static async Task<string> GetDetailContentAsync(string articlePath, ILogger _logger)
        {
            try
            {
                using (FileStream fs = new FileStream(articlePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    {
                        string content = await sr.ReadToEndAsync();
                        return content;
                    }
                }
            }
            catch (Exception ex)
            {
                //此处记录异常，可用Cap发布到异常记录服务器
                _logger.LogError(ex, ex.Message);
                return "false";
            }
        }
    }
}
