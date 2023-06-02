using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; }
}

[TestFixture]
public class LINQQueriesTests
{
    private List<Person> people;

    [SetUp]
    public void Setup()
    {
        // Initialize the list of Person objects
        people = new List<Person>
        {
            new Person { Name = "Alice", Age = 25, Hobbies = new List<string> { "Reading", "Gardening" } },
            new Person { Name = "Bob", Age = 30, Hobbies = new List<string> { "Cooking", "Painting" } },
            new Person { Name = "Charlie", Age = 35, Hobbies = new List<string> { "Gaming", "Singing" } },
            new Person { Name = "David", Age = 40, Hobbies = new List<string> { "Reading", "Writing" } },
            new Person { Name = "Eve", Age = 45, Hobbies = new List<string> { "Cooking", "Singing" } }
        };
    }

    [Test]
    public void Query1_FindPeopleWithAgeBetween30And40AndHobbiesIncludeReading_ReturnsCorrectPeople()
    {
        // Act
        Setup();
        var result = (from p in people where (p.Age >= 30 && p.Age <= 40 && p.Hobbies.Contains("Reading")) select p.Name).ToList();




        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.Contains("David", result);
    }

    [Test]
    public void Query2_FindNamesOfPeopleWithAtLeastTwoHobbies_ReturnsCorrectNames()
    {
        // Act
        //var result = new List<String>();
        Setup();
        var result = (from p in people where (p.Hobbies.Count>=2) select p.Name).ToList();
        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Bob", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("Eve", result);
        Assert.Contains("David", result);
        Assert.Contains("Alice", result);
    }

    [Test]
    public void Query3_CountTotalNumberOfHobbiesAcrossAllPeople_ReturnsCorrectCount()
    {
        // Act
        //var result = new List<String>();
        Setup();
        var result = (from p in people select p.Hobbies.Count()).Sum();
        // Assert
        Assert.AreEqual(10, result);
    }

    [Test]
    public void Query5_CheckIfPersonWithNameContainingAllVowelsExists_ReturnsCorrectResult()
    {
        // Act
        Setup();
        var resulttmp = (from p in people where p.Name.Contains("a") && p.Name.Contains("e") && p.Name.Contains("i") && p.Name.Contains("u") && p.Name.Contains("o") select p).ToList(); 
        var result = false;
        if (resulttmp.Count() != 0)
        {
            result = true;
        }
        //true;

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query6_FindPeopleWhoseNameStartsWithAAndAgeIsLessThan30_ReturnsCorrectPeople()
    {
        // Act
        //var result = new List<String>();
        Setup();
        var result = (from p in people where p.Name.StartsWith('A') && p.Age <30 select p.Name).ToList();
        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.Contains("Alice", result);
    }

    [Test]
    public void Query7_FindOldestPerson_ReturnsCorrectPerson()
    {
        // Act
        // var result = new Person();
        Setup();
        var result = people.MaxBy(x => x.Age);

        // Assert
        Assert.AreEqual("Eve", result.Name);
    }

    [Test]
    public void Query8_CheckIfAnyPersonHasHobbiesStartingWithG_ReturnsCorrectResult()
    {
        // Act
        var tmp = people.Where(x => x.Hobbies.Any(x => x.StartsWith('G'))).ToList();
        var result = true;
        if (tmp.Count == 0)
        {
            result = false;
        }
        

        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public void Query10_CountPeopleWithHobbiesStartingWithS_ReturnsCorrectCount()
    {
        // Act
        var result = people.Where(x => x.Hobbies.Any(x => x.StartsWith('S'))).ToList().Count();

        // Assert
        Assert.AreEqual(2, result);
    }
    
    [Test]
    public void Query11_SelectPeopleWithNamesContainingExactlyFiveLetters_ReturnsCorrectPeople()
    {
        // Act
        var result = (from p in people where p.Name.Length == 5 select p.Name).ToList();
        

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.Contains("Alice", result);
        Assert.Contains("David", result);
    }
    
    [Test]
    public void Query12_CheckIfAllPeopleAreAbove20YearsOld_ReturnsCorrectResult()
    {
        // Act
        var tmp = from p in people
                  where (p.Age >= 20)
                  select p;
        var result = true;
        if (tmp.Count() != people.Count())
        {
            result = false;
        }
        

        // Assert
        Assert.IsTrue(result);
    }

    
    [Test]
    public void Query13_SelectCommonHobbies_ReturnsCorrectHobbies() // hobbies that are shared by at least two people
    {
        // Act
        var result = people.Select(x => x.Hobbies.GroupBy(x => x.GroupBy(x => x).OrderByDescending(x => x.Count()))).Take(3).Select(x => x).ToList();
        //.ToList().GroupBy(x =>x ).OrderByDescending(c => c.Count()).Take(3).Select(x=> x.Key).ToList();
        var tmp = (from p in people select p.Hobbies).Cast<string>;
        // Assert
        Assert.AreEqual(3, result.Count);
        Assert.Contains("Reading", result);
        Assert.Contains("Cooking", result);
        Assert.Contains("Singing", result);
    }

    /*
    [Test]
    public void Query14_FindPeopleWithTwoDistinctHobbies_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("David", result);
        Assert.Contains("Alice", result);
        Assert.Contains("Bob", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("Eve", result);
    }

    [Test]
    public void Query15_SelectHobbiesInAlphabeticalOrder_ReturnsCorrectOrder()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(10, result.Count());
        Assert.AreEqual("Cooking", result.First());
        Assert.AreEqual("Writing", result.Last());
    }

    [Test]
    public void Query16_CheckIfAnyPersonHasNameWithDuplicateCharacters_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query17_SelectPeopleWithAtLeastOneHobbyStartingWithR_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.Contains("David", result);
        Assert.Contains("Alice", result);
    }

    [Test]
    public void Query18_FindPersonWithLongestName_ReturnsCorrectPerson()
    {
        // Act
        var result = new Person();

        // Assert
        Assert.AreEqual("Charlie", result.Name);
    }

    [Test]
    public void Query19_CheckIfAllPeopleHaveUniqueHobbies_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query20_SelectPeopleWithAtLeastOneHobbyContainingMoreThanTwoWords_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query21_FindPeopleWithNamesEndingWithEOrContainsVowelsAndHaveAtLeastOneHobby_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Alice", result);
        Assert.Contains("Bob", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("David", result);
        Assert.Contains("Eve", result);
    }

    [Test]
    public void Query22_CountPeopleWithTotalAgeGreaterThan150_ReturnsCorrectCount()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(175, result);
    }

    [Test]
    public void Query24_FindPeopleWithNamesContainingOnlyLetters_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Alice", result);
        Assert.Contains("Bob", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("David", result);
        Assert.Contains("Eve", result);
    }

    [Test]
    public void Query25_CheckIfAnyPersonHasAllHobbiesStartingWithC_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query26_SelectPeopleWithDistinctAges_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<Person>();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Alice", result.Select(p => p.Name).ToList());
        Assert.Contains("Bob", result.Select(p => p.Name).ToList());
        Assert.Contains("Charlie", result.Select(p => p.Name).ToList());
        Assert.Contains("David", result.Select(p => p.Name).ToList());
        Assert.Contains("Eve", result.Select(p => p.Name).ToList());
    }

    [Test]
    public void Query27_CountPeopleWithNameContainingVowelsInReverseOrder_ReturnsCorrectCount()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(5, result);
    }

    [Test]
    public void Query28_SelectPeopleWithNamesOfEvenLength_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query29_CheckIfAllPeopleHaveUniqueNames_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query30_FindPeopleWithNamesContainingDuplicateLetters_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(3, result.Count());
        Assert.Contains("Bob", result.ToList());
        Assert.Contains("David", result.ToList());
        Assert.Contains("Eve", result.ToList());
    }

    [Test]
    public void Query31_SelectPeopleWithMultipleHobbiesContainingTheWordIn_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Bob", result.ToList());
        Assert.Contains("David", result.ToList());
        Assert.Contains("Eve", result.ToList());
        Assert.Contains("Alice", result.ToList());
        Assert.Contains("Charlie", result.ToList());
    }

    [Test]
    public void Query33_CheckIfAnyPersonHasAgeGreaterThan50AndHobbiesStartingWithG_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query34_CountPeopleWithHobbiesContainingMoreThanTwoWords_ReturnsCorrectCount()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(0, result);
    }

    [Test]
    public void Query36_FindPeopleWithNamesContainingOnlyUppercaseLetters_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query37_CheckIfAllPeopleHaveAtLeastTwoHobbies_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query38_SelectPeopleWithNamesContainingMoreThanTwoWords_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query40_FindPersonWithShortestName_ReturnsCorrectPerson()
    {
        // Act
        var result = new Person();

        // Assert
        Assert.AreEqual("Bob", result.Name);
    }

    [Test]
    public void Query41_SelectPeopleWithAtLeastOneHobbyContainingMoreThanEightCharacters_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.Contains("Alice", result);
    }

    [Test]
    public void Query42_CheckIfAnyPersonHasAllHobbiesContainingTheLetterO_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query44_FindPeopleWithNamesContainingDigits_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query45_CheckIfAllPeopleHaveDistinctHobbies_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query49_CheckIfAnyPersonHasAgeGreaterThan60AndNameContainingLetterX_ReturnsCorrectResult()
    {
        // Act
        var result = true;

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query50_SelectPeopleWithNamesContainingDuplicateWords_ReturnsCorrectPeople()
    {
        // Act
        var result = new List<String>();

        // Assert
        Assert.AreEqual(0, result.Count());
    }*/
}