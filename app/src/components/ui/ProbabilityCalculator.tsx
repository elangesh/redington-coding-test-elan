import { ReactNode } from "react";

interface ProbabilityCalculatorProps {
  children: ReactNode;
}
function ProbabilityCalculator({ children }: ProbabilityCalculatorProps) {
  return (
    <div className="min-h-screen bg-gray-50 px-4 py-12 sm:px-6 lg:px-8">
      <div className="mx-auto max-w-md overflow-hidden rounded-xl bg-white p-6 shadow-md md:max-w-2xl">
        {children}
      </div>
    </div>
  );
}

export default ProbabilityCalculator;
