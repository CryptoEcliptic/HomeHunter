using HomeHunter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Data.DataSeeding
{
    public class NeighbourhoodSeeder : ISeeder
    {
        private readonly string[] SofiaNeighbourhoods = new string[] {
            "Абдовица",
            "Банишора",
            "Барите",
            "Белите брези (квартал)",
            "Бенковски (квартал)",
            "Бизнес парк София",
            "Бокар",
            "Борово (квартал)",
            "Ботунец",
            "Бояна",
            "Бункера",
            "Бъкстон",
            "Витоша (квартал)",
            "Владимир Заимов (квартал)",
            "Враждебна",
            "Връбница (квартал)",
            "Връбница-1",
            "Връбница-2",
            "Гевгелийски",
            "Гео Милев",
            "Горна баня",
            "Горубляне",
            "Гоце Делчев (квартал)",
            "Дианабад",
            "Димитър Миленков",
            "Драгалевци",
            "Драз махала",
            "Дружба",
            "Дървеница",
            "Западен парк",
            "Захарна фабрика",
            "Зона Б-5",
            "Зона Б-18",
            "Зона Б-19",
            "Иван Вазов (квартал)",
            "Изгрев (квартал)",
            "Изток (квартал)",
            "Илинден (жилищен комплекс)",
            "Илиянци",
            "Киноцентър",
            "Княжево",
            "Красна поляна (квартал)",
            "Красно село",
            "Кремиковци",
            "Крива река (квартал)",
            "Кръстова вада",
            "Лагера",
            "Лев Толстой (жилищен комплекс)",
            "Левски (квартал)",
            "Лозенец",
            "Люлин",
            "Малашевци",
            "Малинова долина",
            "Манастирски ливади",
            "Младост",
            "Модерно предградие",
            "Момкова махала",
            "Мотописта",
            "Мусагеница",
            "Надежда",
            "Обеля",
            "Овча купел",
            "Орландовци",
            "Павлово",
            "Подуяне",
            "Полигона",
            "Разсадника",
            "Редута",
            "Република",
            "Света Троица",
            "Свобода",
            "Сердика",
            "Сеславци",
            "Симеоново",
            "Славия",
            "Слатина",
            "Стефан Караджа",
            "Стрелбище",
            "Студентски град",
            "Сухата река",
            "Суходол",
            "Требич",
            "Триъгълника",
            "Факултета",
            "Филиповци",
            "Фондови жилища",
            "Хаджи Димитър",
            "Хиподрума",
            "Хладилника",
            "Христо Ботев",
            "Цариградски комплекс",
            "Център",
            "Челопечене",
            "Чепинско шосе",
            "Южен парк (квартал)",
            "Яворов (жилищен комплекс)"
        };

        public async Task SeedAsync(HomeHunterDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedNeighbourhoodsAsync(SofiaNeighbourhoods, dbContext);
        }

        private static async Task SeedNeighbourhoodsAsync(string[] neighbourhoods, HomeHunterDbContext dbContext)
        {
            var neighbourhoodsFromDb = dbContext.Neighbourhoods.ToList();
            var createdNeighbourhoods = new List<Neighbourhood>();

            foreach (var name in neighbourhoods)
            {
                if (!neighbourhoodsFromDb.Any(x => x.Name == name))
                {
                    createdNeighbourhoods.Add(new Neighbourhood
                    {
                        Name = name,
                        CreatedOn = DateTime.UtcNow,
                        CityId = 1,
                    });
                }
            }

            await dbContext.Neighbourhoods.AddRangeAsync(createdNeighbourhoods);
            await dbContext.SaveChangesAsync();
        }
    }
}
