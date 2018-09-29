using IES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Data
{
    public static class IESDbInitializer
    {
        public static void Initializer(IESContext context)
        {
            context.Database.EnsureCreated();

            if (context.Departamentos.Any())
            {
                return;
            }

            var departamentos = new Departamento[]
            {
                new Departamento { Nome="Ciência da Computação"},
                new Departamento { Nome="Ciência de Alimentos"}
            };

            foreach(Departamento d in departamentos)
            {
                context.Departamentos.Add(d);
            }

            context.SaveChanges();
        }
    }
}
