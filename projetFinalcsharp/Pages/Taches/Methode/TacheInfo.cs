namespace projetFinalcsharp.Pages.Taches.Methode
{
    public class TacheInfo
    {
        public int id { get; set; }
        public int employeId { get; set; }
        public string description { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public string statut { get; set; }
    }
}