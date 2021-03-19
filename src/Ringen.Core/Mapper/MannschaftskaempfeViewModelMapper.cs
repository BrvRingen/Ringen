﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.CS;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Mapper
{
    public class MannschaftskaempfeViewModelMapper
    {
        public MannschaftskampfViewModel Map(Mannschaftskampf model)
        {
            var viewModel = new MannschaftskampfViewModel(model);
            return viewModel;
        }

        public List<MannschaftskampfViewModel> Map(IEnumerable<Mannschaftskampf> modelListe)
        {
            return modelListe.Select(model => Map(model)).ToList();
        }
    }
}