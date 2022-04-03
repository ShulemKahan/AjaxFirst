﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AjaxDemo.Models;

namespace AjaxDemo.Controllers
{
    public class PeopleController : Controller
    {

        private string _connectionString =
            @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var repo = new PeopleDb(_connectionString);
            List<Person> people = repo.GetAll();
            return Json(people);
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            var repo = new PeopleDb(_connectionString);
            repo.AddPerson(person);
            return Json(person);
        }
        [HttpPost]
        public void Delete(int id)
        {
            var repo = new PeopleDb(_connectionString);
            repo.Delete(id);
        }

        [HttpPost]
        public void Edit(Person person)
        {
            var repo = new PeopleDb(_connectionString);
            repo.Edit(person);
        }
    }
}
