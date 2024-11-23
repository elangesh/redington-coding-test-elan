interface ProbabilityInputProps {
  type: "A" | "B";
  probabilityValue: number;
  isLoading: boolean;
  onValueChange: (value: string) => void;
  onAdjust: (adjustment: number) => void;
}

function ProbabilityInput({
  type,
  probabilityValue,
  isLoading,
  onValueChange,
  onAdjust,
}: ProbabilityInputProps) {
  return (
    <div className="mb-6">
      <div className="mb-2 flex items-center justify-start">
        <label className="text-sm font-medium text-gray-700">
          Probability {type}
        </label>
        <span className="mx-2 text-xs font-semibold italic text-gray-500">
          (Probability Range: 0 to 1)
        </span>
      </div>
      <div className="flex items-center space-x-4">
        <button
          type="button"
          onClick={() => onAdjust(-0.1)}
          className="flex h-10 w-10 items-center justify-center rounded-full bg-gray-200 transition-colors hover:bg-gray-300 disabled:cursor-not-allowed disabled:opacity-50"
          disabled={isLoading || probabilityValue <= 0}
        >
          -
        </button>
        <div className="flex-1 text-center">
          <input
            type="number"
            value={probabilityValue}
            onChange={(e) => onValueChange(e.target.value)}
            step="0.1"
            min="0"
            max="1"
            className="w-24 rounded-lg border px-3 py-2 text-center"
            disabled={isLoading}
          />
        </div>
        <button
          type="button"
          onClick={() => onAdjust(0.1)}
          className="flex h-10 w-10 items-center justify-center rounded-full bg-gray-200 transition-colors hover:bg-gray-300 disabled:cursor-not-allowed disabled:opacity-50"
          disabled={isLoading || probabilityValue >= 1}
        >
          +
        </button>
      </div>
    </div>
  );
}

export default ProbabilityInput;
