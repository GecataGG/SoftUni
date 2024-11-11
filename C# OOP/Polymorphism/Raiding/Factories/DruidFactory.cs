namespace Raiding.Factories;

using Raiding.Interfaces.Factories;
using Raiding.Interfaces.Models;
using Raiding.Models;
using System;

public class DruidFactory : IHeroFactory
{
    public IHero Create(string name) => new Druid(name);
}