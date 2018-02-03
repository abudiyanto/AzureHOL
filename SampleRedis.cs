using Association.Infrastructures;
using Association.Models;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Association.Sample
{
    public class Sample
    {
        
        protected async Task ReadConfiguration(string lang)
        {
            var language = lang ?? "id";
            ViewBag.Authorize = false;
            var stringConfiguration = await Helpers.RedisHelper.GetValue("configuration");
            if (stringConfiguration != null)
            {
                var configuration = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Configuration>>(stringConfiguration));
                ViewBag.HeaderLogo = configuration.Find(x => x.Key == "header-logo").ValueID;
                ViewBag.FooterLogo = configuration.Find(x => x.Key == "footer-logo").ValueID;
                ViewBag.Tagline = configuration.Find(x => x.Key == "tagline").ValueID;
                ViewBag.Title = configuration.Find(x => x.Key == "title").ValueID;
                ViewBag.SiteTitle = configuration.Find(x => x.Key == "site-title").ValueID;
                ViewBag.Description = configuration.Find(x => x.Key == "description").ValueID;
                ViewBag.Keywords = configuration.Find(x => x.Key == "keywords").ValueID;
                ViewBag.Author = configuration.Find(x => x.Key == "default-author").ValueID;
                ViewBag.Image = configuration.Find(x => x.Key == "default-image").ValueID;
                ViewBag.PostSize = configuration.Find(x => x.Key == "post-size").ValueID;
                ViewBag.Favicon = configuration.Find(x => x.Key == "favicon").ValueID;
                ViewBag.Twitter = configuration.Find(x => x.Key == "twitterUrl").ValueID;
                ViewBag.Facebook = configuration.Find(x => x.Key == "facebookUrl").ValueID;
                ViewBag.GooglePlus = configuration.Find(x => x.Key == "googlePlusUrl").ValueID;
                ViewBag.Url = configuration.Find(x => x.Key == "url").ValueID;
            }
            else
            {
                var configuration = await db.Configurations.ToListAsync();
                if (configuration != null)
                {
                    ViewBag.HeaderLogo = configuration.Find(x => x.Key == "header-logo").ValueID;
                    ViewBag.FooterLogo = configuration.Find(x => x.Key == "footer-logo").ValueID;
                    ViewBag.Tagline = configuration.Find(x => x.Key == "tagline").ValueID;
                    ViewBag.Title = configuration.Find(x => x.Key == "title").ValueID;
                    ViewBag.Description = configuration.Find(x => x.Key == "description").ValueID;
                    ViewBag.Keywords = configuration.Find(x => x.Key == "keywords").ValueID;
                    ViewBag.Author = configuration.Find(x => x.Key == "default-author").ValueID;
                    ViewBag.Image = configuration.Find(x => x.Key == "default-image").ValueID;
                    ViewBag.PostSize = configuration.Find(x => x.Key == "post-size").ValueID;
                    ViewBag.Favicon = configuration.Find(x => x.Key == "favicon").ValueID;
                    ViewBag.Twitter = configuration.Find(x => x.Key == "twitterUrl").ValueID;
                    ViewBag.Facebook = configuration.Find(x => x.Key == "facebookUrl").ValueID;
                    ViewBag.GooglePlus = configuration.Find(x => x.Key == "googlePlusUrl").ValueID;
                    ViewBag.Url = configuration.Find(x => x.Key == "url").ValueID;
                    ViewBag.SiteTitle = configuration.Find(x => x.Key == "site-title").ValueID;

                    var value = JsonConvert.SerializeObject(configuration);
                    var task = await Helpers.RedisHelper.SetValue("configuration", value);
                }
            }
        }
        public async Task ClearAllCaches()
        {
            var configurationKey = "configuration";
            var provincesKey = "provinces";
            var task = await Helpers.RedisHelper.Delete(configurationKey);
            task = await Helpers.RedisHelper.Delete(provincesKey);
        }
    }
}