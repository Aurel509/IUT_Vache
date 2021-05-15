using System;

namespace IUT_Vache
{
    class Program
    {
        /*
         * Fonction permettant de vérifier si une valeur 
         *
        */
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


        static void Main(string[] args)
        {
            //Déclaration de variables
            int nombrePiquet = 0;
            double coordX = 0.0;
            double coordY = 0.0;

            //
            while (nombrePiquet < 0 || nombrePiquet == 0 || nombrePiquet >= 50)
            {
                Console.Write("Donnez le nombre de piquets : ");
                nombrePiquet = GetInteger();
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
        }

    }
}
