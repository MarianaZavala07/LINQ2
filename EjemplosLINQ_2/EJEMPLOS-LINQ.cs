using System;
using System.Collections.Generic;
using System.Linq;

// Se usaran varias veces el mismo ejemplo de
// codigo pero usando comando correspondiente
// ThenBy o ThenByDescennding
public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
}
//SE USAN EN EJERCICIOS DE ABAJO COMO JOIN
public class Calificacion
{
    public int EstudianteId { get; internal set; }
    public int Nota { get; internal set; }
}
// SE USA EN EJERCICIOS DE ABAJO COMO JOIN
public class Estudiante
{
    public string Nombre { get; internal set; }
    public int Id { get; internal set; }
}
// SE USA EN GROUPJOIN
public class CategoriaFruta
{
    public int ID { get; set; }
    public string Nombre { get; set; }
}
// SE USA EN GROUPJOIN
public class Fruta
{
    public int CategoriaID { get; set; }
    public string Nombre { get; set; }
}
//SE USARA EN EJEMPLO UNION Y DISTINCT
public class FrutaB
{
    public string Nombre { get; set; }
}
public class Program
{
    public static void Main()
    {
        var personas = new List<Persona>
        {
        new Persona { Nombre = "Mariana", Edad = 24 },
       new Persona { Nombre = "Carlos", Edad = 29 },
        new Persona { Nombre = "Ulises", Edad = 25 }
        };

        var Pe = personas.OrderBy(p => p.Nombre).ThenBy(p => p.Edad);

        foreach (var persona in Pe)
        {
            Console.WriteLine($"Nombre: {persona.Nombre}, Edad: {persona.Edad}");
        }

        var numeros = new List<int> { 1, 2, 3, 4, 5 };
        var cuadrados = numeros.Select(n => n * n);
        //SELECT
        foreach (var cuadrado in cuadrados)
        {
            Console.WriteLine($"No {cuadrado}");
        }
        //SELECT MANY

        var listas = new List<List<int>>
        {
           new List<int> { 1, 2 },
           new List<int> { 3, 4 }
        };

        var SlcMany = listas.SelectMany(lista => lista);

        foreach (var lista in SlcMany)
        {
            Console.WriteLine($"2 listas {lista}");
        }

        var numerosC = new List<int> { 1, 2, 3, 4, 5 };
        var cantidad = numeros.Count();

        foreach (var numeroCount in numerosC)
        {
            Console.WriteLine(numeroCount);
        }

        //AGGREGATE
        var palabras = new List<string> { "Hola", "mundo", "de", "LINQ" };

        string frase = palabras.Aggregate((total, siguiente) => total + " " + siguiente);

        Console.WriteLine($"La frase conectada es: {frase}");

        //CONTAINS

        var ContP = new List<string> { "Hola", "Inge", "Paseme", "Porfa" };

        string palabraABuscar = "Inge";
        bool contiene = palabras.Contains(palabraABuscar);

        if (contiene)
        {
            Console.WriteLine($"La lista contiene la palabra '{palabraABuscar}'.");
        }
        else
        {
            Console.WriteLine($"La lista no contiene la palabra '{palabraABuscar}'.");
        }

        //ANY

        var AnyEj = new List<int> { 1, 2, 3, 4, 5 };


        var pares = numeros.Any(n => n % 2 == 0);


        if (pares)
        {
            Console.WriteLine("Hay números pares en la lista.");
        }
        else
        {
            Console.WriteLine("No hay números pares en la lista.");
        }

        //GROUP BY

        var PersonasGroupBy = new List<Persona>
        {
           new Persona { Nombre = "Ana", Edad = 30 },
           new Persona { Nombre = "Carlos", Edad = 25 },
           new Persona { Nombre = "Elena", Edad = 28 }
        };

        var gruposPorEdad = personas.GroupBy(p => p.Edad);

        //jOIN
        var estudiantes = new List<Estudiante>
       {
    new Estudiante { Id = 1, Nombre = "Juan" },
    new Estudiante { Id = 2, Nombre = "María" }
       };

        var calificaciones = new List<Calificacion>
        {
    new Calificacion { EstudianteId = 1, Nota = 85 },
    new Calificacion { EstudianteId = 2, Nota = 92 }
         };

        var resultado = estudiantes.Join(calificaciones,
        estudiante => estudiante.Id,
        calificacion => calificacion.EstudianteId,
        (estudiante, calificacion) => new { estudiante.Nombre, calificacion.Nota });



        var categorias = new List<CategoriaFruta>
{
    new CategoriaFruta { ID = 1, Nombre = "Cítricos" },
    new CategoriaFruta { ID = 2, Nombre = "Dulces" },
    new CategoriaFruta { ID = 3, Nombre = "De temporada" }
};

        var frutas = new List<Fruta>
{
    new Fruta { CategoriaID = 1, Nombre = "Naranja" },
    new Fruta { CategoriaID = 1, Nombre = "Limón" },
    new Fruta { CategoriaID = 2, Nombre = "Fresa" },
    new Fruta { CategoriaID = 3, Nombre = "Arándano" },
    new Fruta { CategoriaID = 2, Nombre = "Mango" }
};

        var categoriasConFrutas = categorias.GroupJoin(
          frutas,
          categoria => categoria.ID,
          fruta => fruta.CategoriaID,
          (categoria, frutasDeLaCategoria) => new
          {
              NombreCategoria = categoria.Nombre,
              Frutas = frutasDeLaCategoria.Select(f => f.Nombre)
          });

        foreach (var categoria in categoriasConFrutas)
        {
            Console.WriteLine($"{categoria.NombreCategoria} siguiente fruta:");
            foreach (var fruta in categoria.Frutas)
            {
                Console.WriteLine($"- {fruta}");
            }
        }
         
         var lista1 = new List<Fruta>
        {
            new Fruta { Nombre = "Manzana" },
            new Fruta { Nombre = "Banana" },
            new Fruta { Nombre = "Naranja" },
            new Fruta { Nombre = "Manzana" }  
        };

        var lista2 = new List<Fruta>
        {
            new Fruta { Nombre = "Kiwi" },
            new Fruta { Nombre = "Banana" }, 
            new Fruta { Nombre = "Uva" }
        };

        var lista1SinDuplicados = lista1.DistinctBy(f => f.Nombre).ToList();
        var lista2SinDuplicados = lista2.DistinctBy(f => f.Nombre).ToList();

        var listaUnida = lista1SinDuplicados.UnionBy(lista2SinDuplicados, f => f.Nombre).ToList();

        Console.WriteLine("Frutas únicas en la lista combinada:");
        foreach (var fruta in listaUnida)
        {
            Console.WriteLine(fruta.Nombre);
        }



    }




}

