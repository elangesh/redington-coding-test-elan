import config from "../config";
import {
  ProbabilityResponse,
  ErrorResponse,
  CalculationType,
} from "../types/probability";

interface CalculateProbabilityParams {
  probabilityA: number;
  probabilityB: number;
  calculationType: CalculationType;
}

async function calculateProbability({
  probabilityA,
  probabilityB,
  calculationType,
}: CalculateProbabilityParams): Promise<ProbabilityResponse> {
  const endpoint = calculationType === "CombinedWith" ? "combined" : "either";
  const response = await fetch(
    `${config.apiBaseUrl}/Probability/${endpoint}?ProbabilityA=${probabilityA}&ProbabilityB=${probabilityB}`,
  );

  if (!response.ok) {
    const errorData = (await response.json()) as ErrorResponse[];
    throw new Error(errorData[0]?.errorMessage || "An error occurred");
  }

  return (await response.json()) as ProbabilityResponse;
}

export const probabilityService = {
  calculateProbability,
};
