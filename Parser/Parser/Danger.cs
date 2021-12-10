using Microsoft.EntityFrameworkCore;
using System;

namespace Parser
{
    public class Danger
    {
        public int Id { get; set; }

        public int UBID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SourceOfThreat { get; set; }

        public string Object { get; set; }

        public bool BreachOfConfidentiality { get; set; }

        public bool IntegrityViolation { get; set; }

        public bool AccessibilityViolation { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Danger)) return false;
            else
            {
                Danger danger = obj as Danger;
                if (UBID == danger.UBID &&
                    Name == danger.Name &&
                    Description == danger.Description &&
                    SourceOfThreat == danger.SourceOfThreat &&
                    Object == danger.Object &&
                    BreachOfConfidentiality == danger.BreachOfConfidentiality &&
                    IntegrityViolation == danger.IntegrityViolation &&
                    AccessibilityViolation == danger.AccessibilityViolation) return true;
                else return false;
            }
        }

        public override string ToString()
        {
            return $"УБИ.{UBID:000} - {Name}";
        }

        public static string Compare(Danger old, Danger update)
        {
            string s = old.Name;
            if (old.Name != update.Name)s += $"/{update.Name}";
            s += $"\r\n{old.Description}";
            if (old.Description != update.Description) s += $"/{update.Description}";
            s += $"\r\n{old.SourceOfThreat}";
            if (old.SourceOfThreat != update.SourceOfThreat) s += $"/{update.SourceOfThreat}";
            s += $"\r\n{old.Object}";
            if (old.Object != update.Object) s += $"/{update.Object }";
            s += $"\r\n{old.BreachOfConfidentiality}";
            if (old.BreachOfConfidentiality != update.BreachOfConfidentiality) s += $"/{update.BreachOfConfidentiality }";
            s += $"\r\n{old.IntegrityViolation}";
            if (old.IntegrityViolation != update.IntegrityViolation) s += $"/{update.IntegrityViolation}";
            s += $"\r\n{old.AccessibilityViolation}";
            if (old.AccessibilityViolation != update.AccessibilityViolation) s += $"/{update.AccessibilityViolation}";
            return s;
        }
    }

    public class AppDbContext : DbContext
    {
        public static AppDbContext db = new AppDbContext();

        public DbSet<Danger> Danger { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=DangerDB.db");
        }
    }

    
}
