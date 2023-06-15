using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using module_21.Models;
using System.Diagnostics;

namespace module_21.Controllers
{
    public class HomeController : Controller //контроллер управления главной страницей и абонентами
    {
        public ApplicationContext db;
        public ILogger<HomeController> _logger;

        public HomeController(ApplicationContext context, ILogger<HomeController> logger)
        {
            db = context;
            _logger = logger;

            //создание абонентов если их нет
            if (!db.Subscribers.Any())
            {
                db.Subscribers.Add(new Subscriber { Name = "Тимур", Surname = "Савельев", Patronymic = "Валерьевич", PhoneNumber = "+7(950)124-30-66", Address = "360377, Липецкая область, город Наро-Фоминск, наб. Балканская, 01", Description = "описание 1", });
                db.Subscribers.Add(new Subscriber { Name = "Юлиан", Surname = "Веселов", Patronymic = "Ефимович", PhoneNumber = "+7(910)270-11-07", Address = "397816, Владимирская область, город Солнечногорск, въезд Сталина, 79", Description = "описание 2", });
                db.SaveChanges();
            }
        }

        //главная страница с выводом всех абонентов
        public async Task<IActionResult> Index()
        {            
            return View(await db.Subscribers.ToListAsync());
        }

        //страница создания абонента
        public IActionResult CreateSubscriber()
        {
            return View();
        }

        //создание абонента
        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(Subscriber subscriber)
        {
            db.Subscribers.Add(subscriber);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //страница редактирования абонента
        [Authorize(Roles = "admin")] //ограничение прав доступа
        public async Task<IActionResult> EditSubscriber(int? id)
        {
            if (id != null)
            {
                Subscriber subscriber = await db.Subscribers.FirstOrDefaultAsync(p => p.Id == id);
                if (subscriber != null)
                    return View(subscriber);
            }
            return NotFound();
        }

        //редактирование абонента
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditSubscriber(Subscriber subscriber)
        {
            db.Subscribers.Update(subscriber);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //подтверждение удаления абонента
        [Authorize(Roles = "admin")] //ограничение прав доступа
        [HttpGet]
        [ActionName("DeleteSubscriber")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Subscriber subscriber = await db.Subscribers.FirstOrDefaultAsync(p => p.Id == id);
                if (subscriber != null)
                    return View(subscriber);
            }
            return NotFound();
        }

        //удаление абонента 
        [Authorize(Roles = "admin")] //ограничение прав доступа
        [HttpPost]
        public async Task<IActionResult> DeleteSubscriber(int? id)
        {
            if (id != null)
            {
                Subscriber subscriber = await db.Subscribers.FirstOrDefaultAsync(p => p.Id == id);
                if (subscriber != null)
                {
                    db.Subscribers.Remove(subscriber);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}