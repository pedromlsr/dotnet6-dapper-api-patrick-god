using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperCrudTutorial
{
    public class SuperHero
    {
        public int Id { get; set; }
        public string Codename { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
}
