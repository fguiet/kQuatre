using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guiet.kQuatre.Business
{
    public enum FireworkStatus
    {
        Running, //En cours de fonctionnement
        StoppedAndNeverLaunched, //Arrêté mais jamais lancé
        StoppedByUserBeforeEnd, //Arrêté et l'utilisateur a lancé le feu
        Paused, //En pause
        StoppedAndFinished //Arrêté et fini complètement       
    }
}
