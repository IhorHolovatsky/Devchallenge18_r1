using System.Threading.Tasks;
using BoxCutting.Core.Dtos;

namespace BoxCutting.Core.Interfaces
{
    public interface ISimpleBoxService
    {
        Task<SimpleBoxResponseDto> GetCuttingProgramAsync(SimpleBoxRequestDto requestDto);
    }
}