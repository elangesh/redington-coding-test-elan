export type CalculationType = "CombinedWith" | "Either";

export interface ProbabilityResponse {
  result: number;
  calculationType: "CombinedWith" | "Either";
  input: {
    probabilityA: number;
    probabilityB: number;
  };
  calculationTime: string;
}

export interface ErrorResponse {
  propertyName: string;
  errorMessage: string;
  attemptedValue: number;
  customState: null;
  severity: number;
  errorCode: string;
  formattedMessagePlaceholderValues: {
    From: number;
    To: number;
    PropertyName: string;
    PropertyValue: number;
  };
}

export interface State {
  probabilityA: number;
  probabilityB: number;
  calculationType: CalculationType;
  result: number | null;
  isLoading: boolean;
  error: string | null;
}

export type Action =
  | { type: "calculation/probabilityA"; payload: number }
  | { type: "calculation/probabilityB"; payload: number }
  | { type: "calculation/calculationType"; payload: CalculationType }
  | { type: "calculation/result"; payload: number }
  | { type: "calculation/loading"; payload: boolean }
  | { type: "calculation/error"; payload: string | null };
