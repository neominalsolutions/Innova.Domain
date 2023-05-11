// See https://aka.ms/new-console-template for more information
using Account.Domain.SeedWorks;

Console.WriteLine("Hello, World!");



Location l1 = new Location(23.563, 24.789);
// l1.Lat = 5;

Location l2 = new Location(23.564, 24.786);

Console.WriteLine("l1",l1.ToString());
Console.WriteLine("l2", l2.ToString());


var r1 = Location.Equals(l1, l2); // l1 ile l2 aynı değerlere mi eşit true
var r2 = Location.ReferenceEquals(l1, l2); // aynı class referans mı false


Console.WriteLine($"eşit mi {l1.Equals(l2)}");
