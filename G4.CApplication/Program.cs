using G4.Repository.Controllers;
using G4.Repository.Models;

var menu = "";

var bDao = new BrandDao();

while(!menu.Equals("5")) {

    Console.WriteLine("== Brands ==");
    Console.WriteLine("= 1 - List");
    Console.WriteLine("= 2 - Insert One");
    Console.WriteLine("= 3 - Update One");
    Console.WriteLine("= 4 - Delete One");
    Console.WriteLine("= 5 - Exit \n");

    menu = Console.ReadLine();

    if (menu == null)
        menu = "";

    if(menu.Equals("1")) {

        var brands = bDao.GetAll();

        foreach(var brand in brands) {
            Console.WriteLine($"\nID         : {brand.id}");
            Console.WriteLine($"NAME       : {brand.name}");
            Console.WriteLine($"FOUNDATION : {brand.fundationDate.ToString("dd/MM/yyyy")}\n");
        }

    }

    if (menu.Equals("2")) {

        var brand = new Brand();

        Console.WriteLine("Brand Name: ");
        var name = Console.ReadLine();

        Console.WriteLine("Foundation Date: ");
        var foundation = Console.ReadLine();

        try {

            if (name == null || foundation == null)
                throw new Exception("Brand Name or Foundation Date Null");

            brand.name = name;
            brand.fundationDate = DateTime.Parse(foundation);

            bDao.Insert(brand);

            Console.WriteLine("== Success ==\n");

        } catch(Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }

    if (menu.Equals("3")) {

        Console.WriteLine("Brand ID: ");
        var id = Console.ReadLine();

        if (id == null) {
            Console.WriteLine("= Wrong Format ID");
        } else {

            var brand = bDao.FindByID(id);

            if(brand == null) {
                Console.WriteLine("= Not Found ID");
            } else {

                Console.WriteLine("Founded !! Leave blank to ignore the field in update");

                Console.WriteLine("\n= Brand");
                Console.WriteLine($"Current : {brand.name}");
                Console.WriteLine("New : ");
                var name = Console.ReadLine();

                Console.WriteLine("\n= Foundation Date");
                Console.WriteLine($"Current: {brand.fundationDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine("New : ");
                var foundation = Console.ReadLine();

                try {

                    if (name != null)
                        brand.name = name;

                    if (foundation != null)
                        if(!foundation.Equals("")) 
                            brand.fundationDate = DateTime.Parse(foundation);

                    var updateResult = bDao.Update(brand);

                    Console.WriteLine(updateResult ? "= Success" : "= Error");

                } catch (Exception ex) {
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
        }

    }

    if (menu.Equals("4")) {

        Console.WriteLine("Brand ID: ");
        var id = Console.ReadLine();

        if(id == null) {
            Console.WriteLine("= Wrong Format ID");
        } else {
            var deleted = bDao.Delete(id);

            Console.WriteLine(deleted ? "= Success" : "= Not Found");
        }

    }

}




