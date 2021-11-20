using System.Threading.Tasks;
using BoxCutting.Core.Dtos;
using BoxCutting.Core.Interfaces;
using BoxCutting.Core.Models;

namespace BoxCutting.Core.Services
{
    public class SimpleBoxService : ISimpleBoxService
    {
        public SimpleBoxService()
        {

        }


        public Task<SimpleBoxResponseDto> GetCuttingProgramAsync(SimpleBoxRequestDto requestDto)
        {
            var boxCutter = new BoxCutter(new Sheet(requestDto.Sheet.Width, requestDto.Sheet.Length));

            var result = boxCutter.GetCuttingProgram(
                requestDto.BoxSize.Width,
                requestDto.BoxSize.Depth,
                requestDto.BoxSize.Height);

            return Task.FromResult(new SimpleBoxResponseDto
            {
                Error = result.Error,
                Amount = result.Amount,
                Program = result.Program
            });
        }
    }
}