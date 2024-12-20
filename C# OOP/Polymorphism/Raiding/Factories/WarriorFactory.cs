﻿namespace Raiding.Factories;

using Raiding.Interfaces.Factories;
using Raiding.Interfaces.Models;
using Raiding.Models;
using System.Threading;

public class WarriorFactory : IHeroFactory
{
    public IHero Create(string name) => new Warrior(name);
}