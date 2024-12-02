namespace JaratKezeloProjekt
{
    public class Jaratkezelo
    {
        List<Jarat> JaratkezeloLista = new List<Jarat>();
        void UjJarat(string jaratSzam, string repterHonnan, string repterHova, DateTime indulas)
        {
            if (jaratSzam == "")
            {
                throw new ArgumentException("A járatszám nem lehet üres!", nameof(jaratSzam));
            }
            if (repterHonnan == "")
            {
                throw new ArgumentException("A reptér honnan lehet üres!", nameof(repterHonnan));
            }
            if (repterHova == "")
            {
                throw new ArgumentException("A reptér hova nem lehet üres!", nameof(repterHova));
            }
            if (indulas == null)
            {
                throw new ArgumentException("Az indulás nem lehet üres!", nameof(indulas));
            }
            foreach (var jarat in JaratkezeloLista)
            {
                if (jarat.jaratSzam == jaratSzam)
                {
                    throw new InvalidOperationException("A járat már létezik!");
                }
            }


            Jarat ujJarat = new Jarat();
            ujJarat.keses = 0;
            ujJarat.jaratSzam = jaratSzam;
            ujJarat.repterHonnan = repterHonnan;
            ujJarat.repterHova = repterHova;
            ujJarat.indulas = indulas;
            JaratkezeloLista.Add(ujJarat);
        }


        void Keses(string jaratSzam, int keses)
        {

            if (keses == 0)
            {
                throw new ArgumentException("A ksés nem lehet nulla!", nameof(keses));
            }
            int index = 0;
            foreach (var jarat in JaratkezeloLista)
            {
                if (jarat.jaratSzam == jaratSzam)
                {
                    index++;
                }
            }
            if (index == 0)
            {
                throw new ArgumentException("Nincs ilyen járat!", nameof(jaratSzam));
            }


            foreach (var jarat in JaratkezeloLista)
            {
                if (jarat.jaratSzam == jaratSzam)
                {
                    if (jarat.keses + keses < 0)
                    {
                        throw new NegativKesesException(jaratSzam + " járat késése nem lehet negatív!", nameof(keses));
                    }
                    jarat.keses += keses;
                    break;
                }
            }


        }


        public DateTime MikorIndul(string jaratSzam)
        {
            

            foreach (var jarat in JaratkezeloLista)
            {
                if (jarat.jaratSzam == jaratSzam)
                {
                    return jarat.indulas.AddMinutes(jarat.keses);
                }
            }
            throw new ArgumentException("Nincs ilyen járat!", nameof(jaratSzam));

        }
    




        private class Jarat
        {
            public string jaratSzam;
            public string repterHonnan;
            public string repterHova;
            public DateTime indulas;
            public int keses;
        }
    }

    [Serializable]
    internal class NegativKesesException : Exception
    {
        private string v1;
        private string v2;

        public NegativKesesException()
        {
        }

        public NegativKesesException(string? message) : base(message)
        {
        }

        public NegativKesesException(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public NegativKesesException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
