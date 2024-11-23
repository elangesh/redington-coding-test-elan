import ProbabilityInput from "./ProbabilityInput.tsx";

interface ProbabilityInputGroupProps {
  probabilityA: number;
  probabilityB: number;
  isLoading: boolean;
  onValueChange: (type: "A" | "B", value: string) => void;
  onAdjust: (type: "A" | "B", adjustment: number) => void;
}

function ProbabilityInputGroup({
  probabilityA,
  probabilityB,
  isLoading,
  onValueChange,
  onAdjust,
}: ProbabilityInputGroupProps) {
  return (
    <div>
      {["A", "B"].map((prob) => (
        <ProbabilityInput
          key={prob}
          type={prob as "A" | "B"}
          probabilityValue={prob === "A" ? probabilityA : probabilityB}
          isLoading={isLoading}
          onValueChange={(value) => onValueChange(prob as "A" | "B", value)}
          onAdjust={(adjustment) => onAdjust(prob as "A" | "B", adjustment)}
        />
      ))}
    </div>
  );
}

export default ProbabilityInputGroup;
