using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrudTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SuperHeroController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetAllSuperHeroes()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            IEnumerable<SuperHero> heroes = await SelectAllHeroes(connection);

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHeroById(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var sql = "SELECT super_hero_id as Id, codename, first_name as FirstName, last_name as LastName, place FROM super_heroes WHERE super_hero_id = @Id";

            var hero = await connection.QueryFirstAsync<SuperHero>(sql, new { Id = id });

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SuperHero>>> CreateSuperHero(SuperHero superHero)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var sql = "INSERT INTO super_heroes (" +
                "codename," +
                "first_name," +
                "last_name," +
                "place" +
                ")" +
                "\nVALUES (" +
                "@Codename," +
                "@FirstName," +
                "@LastName," +
                "@Place" +
                ")";

            await connection.ExecuteAsync(sql, superHero);

            return Ok(await SelectAllHeroes(connection));
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<SuperHero>>> UpdateSuperHero(SuperHero superHero)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var sql = "UPDATE super_heroes" +
                "\nSET " +
                "codename = @Codename," +
                "first_name = @FirstName," +
                "last_name = @LastName," +
                "place = @Place" +
                "\nWHERE super_hero_id = @Id";

            await connection.ExecuteAsync(sql, superHero);

            return Ok(await SelectAllHeroes(connection));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<SuperHero>>> DeleteSuperHero(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var sql = "DELETE FROM super_heroes WHERE super_hero_id = @Id";

            await connection.ExecuteAsync(sql, new { Id = id });

            return Ok(await SelectAllHeroes(connection));
        }

        private static async Task<IEnumerable<SuperHero>> SelectAllHeroes(SqlConnection connection)
        {
            var sql = "SELECT super_hero_id as Id, codename, first_name as FirstName, last_name as LastName, place FROM super_heroes";

            var heroes = await connection.QueryAsync<SuperHero>(sql);

            return heroes;
        }
    }
}
