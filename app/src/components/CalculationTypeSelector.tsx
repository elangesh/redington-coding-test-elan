import { CalculationType } from "../types/probability.ts";

interface CalculationTypeSelectorProps {
  selectedType: CalculationType;
  onTypeChange: (type: CalculationType) => void;
}

function CalculationTypeSelector({
  selectedType,
  onTypeChange,
}: CalculationTypeSelectorProps) {
  return (
    <div className="mb-8">
      <div className="flex justify-center space-x-4">
        {["CombinedWith", "Either"].map((type) => (
          <button
            key={type}
            onClick={() => onTypeChange(type as CalculationType)}
            className={`rounded-lg px-4 py-2 ${
              selectedType === type
                ? "bg-blue-600 text-white"
                : "bg-gray-100 text-gray-700 hover:bg-gray-200"
            } transition-colors duration-200`}
          >
            {type === "CombinedWith" ? "Combined With" : "Either"}
          </button>
        ))}
      </div>
    </div>
  );
}

export default CalculationTypeSelector;
