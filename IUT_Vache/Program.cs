using System;

namespace IUT_Vache
{
    class Program
    {
        /// <summary>
        /// Vérifie si l'entier donné est un entier
        /// </summary>
        /// <returns>Entier écrit par l'utilisateur dans la console</returns>

        public static int GetInteger()
        {
            int value = 0;
            string stringValue = Console.ReadLine();

            //Vérification de valeur
            try
            {
                value = int.Parse(stringValue);
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur :  L'entier donné est incorrect. Veuillez réessayer.");
            }

            return value;
        }

        /// <summary>
        /// Vérifie si le nombre décimal donné est un nombre décimal
        /// </summary>
        /// <returns>Nombre décimal écrit par l'utilisateur dans la console</returns>
        public static double GetDouble()
        {
            double value = 0.0;
            string stringValue = Console.ReadLine();

            //Vérification de valeur
            try
            {
                value = double.Parse(stringValue);
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur :  Le nombre donné n'est pas dans le bon format. Utilisez ',' et non '.' pour des nombres décimaux.");
            }

            return value;
        }

        /// <summary>
        /// Fonction principale du programme
        /// </summary>
        static void Main(string[] args)
        {
            //Déclaration de variables
            int nombrePiquet = 0;
            double coordX = 0.0;
            double coordY = 0.0;

            //Si le nombre de piquets est incorrect. On demande de resaisir 
            while (nombrePiquet < 3 || nombrePiquet >= 50)
            {
                Console.Write("Donnez le nombre de piquets : ");
                nombrePiquet = GetInteger();


                if (nombrePiquet < 3)
                {
                    Console.WriteLine("Merci de donner 3 piquets au minimum.");
                }
                else if (nombrePiquet >= 50)
                {
                    Console.WriteLine("Vous ne possédez pas autant de piquets.");
                }
            }

            //Saisie de la coordonnée x et y de chaque piquet 

            for(int i = 1; i<=nombrePiquet; i++)
            {
                Console.Write("Saisir le piquet n° " + i);

                while(coordX == 0.0)
                {
                    Console.Write("\nx: ");
                    coordX = GetDouble();
                }

                while (coordY == 0.0)
                {
                    Console.Write("\ny: ");
                    coordY = GetDouble();
                }
                new Piquet(coordX, coordY);
                coordX = 0.0;
                coordY = 0.0;
            }
            Console.WriteLine("Aire : " +  Piquet.GetAire());
            Console.WriteLine("Centre de gravité : (" + Piquet.GetCentreGravite(true) + " , " + Piquet.GetCentreGravite(false) + ")");

            if(Piquet.AppartenancePointPolygone(Piquet.GetList(), Piquet.GetCentreGravite(true), Piquet.GetCentreGravite(false)))
            {
                Console.WriteLine("Votre vache est dans le pré.");
            } else
            {
                Console.WriteLine("Attention, la vache est hors du pré.");
            }
        }

    }
}
