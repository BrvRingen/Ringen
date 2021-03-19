﻿using Ringen.Schnittstellen.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Factories;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.SichtbarkeitsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceErsteller x = new ServiceErsteller();
            var _saisonInformationen = x.GetService<ISaisonInformationen>();

            List<Saison> saisonListe = _saisonInformationen.GetSaisonsAsync().Result;

        }
    }
}
