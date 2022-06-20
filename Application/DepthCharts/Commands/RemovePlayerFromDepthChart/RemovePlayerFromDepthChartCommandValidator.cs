using Application.Common.Interfaces;
using FluentValidation;

namespace Application.DepthCharts.Commands.RemovePlayerFromDepthChart
{
    public class RemovePlayerFromDepthChartCommandValidator : AbstractValidator<RemovePlayerFromDepthChartCommand>
    {
        private IGameService _gameService;

        public RemovePlayerFromDepthChartCommandValidator(IGameService gameService)
        {
            _gameService = gameService;
            RuleFor(v => v.PlayerId)
                .NotEmpty().WithMessage("PlayerId is required");
            RuleFor(v => v.Position)
                .NotNull().WithMessage("Position is required")
                .NotEmpty().WithMessage("Position is required")
                .MaximumLength(50).WithMessage("Position must not exceed 50 characters.")   //Assumption
                .MustAsync(BeSupportedPostion).WithMessage("Position not valid.");
        }

        private async Task<bool> BeSupportedPostion(string position, CancellationToken cancellationToken)
        {
            if (position != null)
            {
                var currentChart = await _gameService.GetFullDepthChart();
                if (currentChart == null)
                {
                    return false;
                }
                if (!currentChart.Chart.ContainsKey(position))
                {
                    return false;

                }
            }
            return true;
        }
    }
}
