using PokemonGBAFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EditorScriptsPokemon.Helper
{
    public class RomPokemon
    {
        public RomGba Rom { get; set; } = default;
        public bool IsLoaded => Rom != default;
    }
}
