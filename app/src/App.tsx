import { useReducer } from "react";
import { CalculationType } from "./types/probability.ts";
import { initialState, reducer } from "./reducers/probabilityReducer.ts";
import Header from "./components/Header.tsx";
import CalculateButton from "./components/CalculateButton.tsx";
import ResultDisplay from "./components/display/ResultDisplay.tsx";
import ErrorDisplay from "./components/display/ErrorDisplay.tsx";
import CalculationTypeSelector from "./components/CalculationTypeSelector.tsx";
import ProbabilityCalculator from "./components/ui/ProbabilityCalculator.tsx";
import ProbabilityInputGroup from "./components/probability-input/ProbabilityInputGroup.tsx";
import { probabilityService } from "./services/probabilityService.ts";

function App() {
  const [state, dispatch] = useReducer(reducer, initialState);

  function adjustProbability(type: "A" | "B", adjustment: number): void {
    const currentValue = type === "A" ? state.probabilityA : state.probabilityB;
    const newValue = Number(
      Math.min(Math.max(currentValue + adjustment, 0), 1).toFixed(1),
    );

    dispatch({
      type:
        type === "A" ? "calculation/probabilityA" : "calculation/probabilityB",
      payload: newValue,
    });
  }

  function handleInputChange(prob: "A" | "B", value: string): void {
    const numValue = parseFloat(value);
    if (!isNaN(numValue)) {
      const validValue = Number(Math.min(Math.max(numValue, 0), 1).toFixed(1));
      dispatch({
        type:
          prob === "A"
            ? "calculation/probabilityA"
            : "calculation/probabilityB",
        payload: validValue,
      });
    }
  }

  function handleCalculationTypeChange(type: CalculationType): void {
    dispatch({ type: "calculation/calculationType", payload: type });
  }

  function handleCalculateClick(e: React.MouseEvent<HTMLButtonElement>): void {
    e.preventDefault();
    void calculateProbability();
  }

  async function calculateProbability(): Promise<void> {
    dispatch({ type: "calculation/loading", payload: true });
    dispatch({ type: "calculation/error", payload: null });

    try {
      const data = await probabilityService.calculateProbability({
        probabilityA: state.probabilityA,
        probabilityB: state.probabilityB,
        calculationType: state.calculationType,
      });

      dispatch({ type: "calculation/result", payload: data.result });
    } catch (error) {
      let errorMessage = "An error occurred";
      if (error instanceof Error) {
        errorMessage = error.message;
      }
      dispatch({ type: "calculation/error", payload: errorMessage });
    } finally {
      dispatch({ type: "calculation/loading", payload: false });
    }
  }

  return (
    <ProbabilityCalculator>
      <Header />
      <CalculationTypeSelector
        selectedType={state.calculationType}
        onTypeChange={handleCalculationTypeChange}
      />
      <ProbabilityInputGroup
        probabilityA={state.probabilityA}
        probabilityB={state.probabilityB}
        isLoading={state.isLoading}
        onValueChange={handleInputChange}
        onAdjust={adjustProbability}
      />
      <CalculateButton
        isLoading={state.isLoading}
        onClick={handleCalculateClick}
      />

      {state.result !== null && <ResultDisplay result={state.result} />}
      {state.error && <ErrorDisplay message={state.error} />}
    </ProbabilityCalculator>
  );
}

export default App;
