using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RobotFactory.Console
{
    public class RobotFactory
    {
        public Robot Build(RobotSpecification specification)
        {
            throw new System.NotImplementedException();
        }
    }

    public record Robot
    {
        public Part Head { get; init; }
        public Part Body { get; init; }
        public Part Arms { get; init; }
        public Part Movement { get; init; }
        public Part Power { get; init; }
    }

    public interface ICostAggregator
    {
        Part FindLowestPrice(PartSpecification partSpecification);
    }

    public class CostAggregator : ICostAggregator
    {
        private readonly IEnumerable<ISupplier> _suppliers;

        public CostAggregator(IEnumerable<ISupplier> suppliers)
        {
            _suppliers = suppliers ?? throw new ArgumentNullException(nameof(suppliers));
        }

        public Part FindLowestPrice(PartSpecification partSpecification)
        {
            return _suppliers.Select(x => x.GetPart(partSpecification)).OrderBy(p => p.Price).FirstOrDefault();
        }

        public IEnumerable<Part> GetParts(PartSpecification specification)
        {
            return _suppliers.SelectMany(x => x.GetParts(specification));
        }
    }

    public record Part
    {
        public string ProductName { get; init; }
        public string Type { get; init; }
        public decimal Price { get; init; }
    }

    public record PartSpecification
    {
        public string Type { get; init; }

    }

    public record RobotSpecification
    {
        public IReadOnlyCollection<Part> Parts { get; set; }
        public PartSpecification Head { get; init; }
        public PartSpecification Body { get; init; }
        public PartSpecification Arms { get; init; }
        public PartSpecification Movement { get; init; }
        public PartSpecification Power { get; init; }
    }

    public interface ISupplier
    {
        public string Name { get;}

        public Part GetPart(PartSpecification specification);

        public IEnumerable<Part> GetParts(PartSpecification specification);
    }

}