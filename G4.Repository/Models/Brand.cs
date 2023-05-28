namespace G4.Repository.Models {
    public class Brand {

        public string id { get; set; } = Guid.NewGuid().ToString();
        public string name { get; set; } = "";
        public DateTime fundationDate { get; set; }

        public Brand() { }

    }
}
