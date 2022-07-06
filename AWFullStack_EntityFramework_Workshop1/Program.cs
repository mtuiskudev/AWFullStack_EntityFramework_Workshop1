using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace AWFullStack_EntityFramework_Workshop1
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Esimerkki miten dbsettiä voi käyttää usingin ulkopuolella myös
            DbSet<Kirja> jotain = null;
            jotain.Add(new Kirja { Kirjailija = "Tom Robython", Nimike = "The Life of Senna", Vuosi = 2005 });*/

            using (var context = new TietokantaDemoContext())
            {
                //Yhden kirjan lisäys
                Kirja nide = new Kirja();
                nide.Kirjailija = "Väinö Linna";
                nide.Nimike = "Tuntematon Sotilas";
                nide.Vuosi = 1954;

                context.Add(nide);
                context.SaveChanges();

                //Virheellinen listan käyttö, ei lisää kirjaa tietokantaan
                List<Kirja> kirjat = context.Kirja.ToList();
                kirjat.Add( new Kirja { Kirjailija = "Tom Robython", Nimike = "The Life of Senna", Vuosi = 2005 });
                context.SaveChanges();

                //Lisää kirjan tietokantaan, koska käyttää dbsettiä
                DbSet<Kirja> k = context.Kirja;
                k.Add(new Kirja { Kirjailija = "Tom Robython", Nimike = "The Life of Senna", Vuosi = 2005 });

                context.SaveChanges();

                //Lisää myös kirjat tietokantaan, sillä AddRange lisää koko listan, jolla on 2 kirjaa tietokantaan. HUOM! Listan sisällön pitää olla samaa tyyppiä kuin tietokannan data
                List<Kirja> k_lista = new List<Kirja>();
                k_lista.Add(new Kirja {Kirjailija = "Jon Krakauer", Nimike = "Into thin Air", Vuosi = 1997  });
                k_lista.Add(new Kirja { Kirjailija = "Joe Simpson", Nimike = "Touching the Void", Vuosi = 1988 });

                context.AddRange(k_lista);
                context.SaveChanges();

            }
        }
    }

    public class TietokantaDemoContext : DbContext
    {
        public DbSet<Kirja> Kirja { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\;Database=Testi_db;Trusted_Connection=True;");
        }
    }

    public class Kirja
    {
        public int Id { get; set; }
        public string Kirjailija { get; set; }
        public string Nimike { get; set; }
        public int Vuosi { get; set; }
    }
}
