using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Tests
{
    public class PeriodTest
    {
        [Theory]
        [InlineData("2022-10-01", "2022-10-02")]
        [InlineData("2022-10-01", "2022-11-02")]
        [InlineData("2022-10-01", "2023-10-01")]
        public void WhenPassingValidDates_ThenPeriodIsInstanciated(string start, string end)
        {
            // arrange
            DateOnly startDate = DateOnly.Parse(start);
            DateOnly endDate = DateOnly.Parse(end);

            // act
            Period p = new Period(startDate, endDate);

            // assert
            Assert.NotNull(p);
            Assert.Equal(p.StartDate, startDate);
            Assert.Equal(p.EndDate, endDate);
        }


        [Theory]
        [InlineData("2022-10-01", "2022-10-01")]
        public void WhenPassingSameDate_ThenThrowsException(string start, string end)
        {
            // arrange
            DateOnly startDate = DateOnly.Parse(start);
            DateOnly endDate = DateOnly.Parse(end);

            // assert
            var ex = Assert.Throws<ArgumentException>(() => 
            
            // act
            new Period(startDate, endDate)
            
            );

            Assert.Equal("invalid arguments: start date >= end date.", ex.Message);
        }


        [Theory]
        [InlineData("2022-10-01", "2022-09-30")]
        [InlineData("2022-10-01", "2022-09-01")]
        [InlineData("2022-10-01", "2021-10-01")]
        public void WhenPassingInvalidDates_ThenThrowsException(string start, string end)
        {
            // arrange
            DateOnly startDate = DateOnly.Parse(start);
            DateOnly endDate = DateOnly.Parse(end);

            // assert
            var ex = Assert.Throws<ArgumentException>(() => 
            
            // act
            new Period(startDate, endDate)
            
            );

            // assert
            Assert.Equal("invalid arguments: start date >= end date.", ex.Message);
        }
    }
}