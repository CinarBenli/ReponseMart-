using Rocket.API;
using System.Collections.Generic;

namespace ReponseMartı
{
    public class Config : IRocketPluginConfiguration
    {
        public string Logo;
        public List<Martılar> Martı;
        public List<Oyuncular> Oyuncu;

        public void LoadDefaults()
        {
            Logo = "http";
            Martı = new List<Martılar>();
            Oyuncu = new List<Oyuncular>();

        }
    }
}