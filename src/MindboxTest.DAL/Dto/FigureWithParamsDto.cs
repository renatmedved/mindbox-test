using MindboxTest.DAL.Tables;
using System;
using System.Collections.Generic;

namespace MindboxTest.DAL.Dto
{
    public class FigureWithParamsDto
    {
        public FigureWithParamsDto(Figure fig, IEnumerable<FigureAreaParam> @params)
        {
            Figure = fig ?? throw new ArgumentNullException(nameof(fig));
            Params = @params ?? throw new ArgumentNullException(nameof(@params));
        }

        public Figure Figure { get; }
        public IEnumerable<FigureAreaParam> Params { get; }
    }
}
