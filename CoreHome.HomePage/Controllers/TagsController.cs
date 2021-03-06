﻿using CoreHome.Data.DatabaseContext;
using CoreHome.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoreHome.HomePage.Controllers
{
    public class TagsController : Controller
    {
        private readonly ArticleDbContext articleDbContext;

        public TagsController(ArticleDbContext articleDbContext)
        {
            this.articleDbContext = articleDbContext;
        }

        public IActionResult Index()
        {
            ViewBag.PageTitle = "Tags";
            return View();
        }

        public IActionResult AllTags()
        {
            List<Tag> tags = articleDbContext.Tags.Include(i => i.ArticleTags).ToList();

            List<List<string>> wordClouds = new List<List<string>>();
            foreach (var tag in tags)
            {
                wordClouds.Add(new List<string>()
                {
                    {tag.TagName },
                    {(tag.ArticleTags.Count*8).ToString() }
                });
            }
            return Json(wordClouds);
        }
    }
}