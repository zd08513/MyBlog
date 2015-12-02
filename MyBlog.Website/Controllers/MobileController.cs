using MyBlog.Entity;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MyBlogs.Website.Controllers
{
    /// <summary>
    /// 手机服务
    /// </summary>
    public class MobileController : Controller
    {
        private IList<string> VideoTypeList { get; set; }

        public MobileController()
        {
            VideoTypeList = new List<string>();
            VideoTypeList.Add("mp4");
            VideoTypeList.Add("flv");
        }

        /// <summary>
        /// 文件上传界面
        /// </summary>
        /// <returns></returns>
        public ActionResult FileUploading()
        {
            return View(new MobileInfo());
        }

        /// <summary>
        /// 文件上传提交
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FileUploading(MobileInfo mobile)
        {
            if (ModelState.IsValid)
            {
                FileInfo file = new FileInfo(mobile.FilePath);
                mobile.FileName = file.Name;
                //保存文件
                var fileBase=Request.Files["FilePath"];
                fileBase.SaveAs(Path.Combine(mobile.SavePath, fileBase.FileName));
                TempData.Add("UpdateStatus", "上传成功！");
            }
            return View(mobile);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Video(string videoType,string path="")
        {
            switch (videoType)
            {
                case"mv":
                    path = @"F:\MV";
                    break;
                case "video":
                case "VideoCatalogue":
                    if(path=="")
                        path = @"F:\video\最新电影";
                    break;
                default:
                    return RedirectToActionPermanent("index", "home");
            }

            IList<VideoInfo> videoList = AddVideo(path);
            return View(videoList);
        }

        /// <summary>
        /// 播放视频
        /// </summary>
        /// <param name="videoPath"></param>
        /// <returns></returns>
        public ActionResult Player(string videoPath, string videoType)
        {
            if(videoPath==null)
                return RedirectToActionPermanent("index", "home");
            string videoname = videoPath.Substring(videoPath.LastIndexOf("\\") + 1);
            string filename = videoPath.Substring(0, videoPath.LastIndexOf('\\'));
            filename = filename.Substring(filename.IndexOf('\\') + 1);
            filename = filename.Replace('\\', '/');

            ViewBag.VideoPath = string.Format("http://{0}/{1}/{2}",Request.Url.Authority.ToString(), filename, videoname);
            ViewBag.VideoType = videoType;
            ViewBag.VideoName = videoname;
            return View();
        }

        /// <summary>
        /// 视频列表
        /// </summary>
        /// <returns></returns>
        public ActionResult VideoList()
        {
            IList<VideoInfo> videoList = AddVideo(@"F:\video",true);
            return View(videoList);
        }

        /// <summary>
        /// 添加视频
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IList<VideoInfo> AddVideo(string path,bool param=false)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            IList<VideoInfo> videoList = new List<VideoInfo>();
            foreach (FileSystemInfo item in dir.GetFileSystemInfos())
            {
                string fullname = item.FullName;
                fullname = fullname.Substring(fullname.LastIndexOf('\\')+1);
                string videotype = fullname.Substring(fullname.LastIndexOf('.') + 1);
                if (!param)
                {
                    //判断视频格式是否符合播放要求
                    if (!VideoTypeList.Contains(videotype)) continue;
                }
                videoList.Add(new VideoInfo
                {
                    Name = fullname,
                    FilePath = item.FullName,
                    VideoType = videotype
                });
            }
            return videoList;
        }
        
    }
}
