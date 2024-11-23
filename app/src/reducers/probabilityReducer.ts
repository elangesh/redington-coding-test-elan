import { Action, State } from "../types/probability.ts";

export const initialState: State = {
  probabilityA: 0.5,
  probabilityB: 0.5,
  calculationType: "CombinedWith",
  result: null,
  isLoading: false,
  error: null,
};

export function reducer(state: State, action: Action): State {
  switch (action.type) {
    case "calculation/probabilityA":
      return { ...state, probabilityA: action.payload, error: null };
    case "calculation/probabilityB":
      return { ...state, probabilityB: action.payload, error: null };
    case "calculation/calculationType":
      return { ...state, calculationType: action.payload, error: null };
    case "calculation/result":
      return { ...state, result: action.payload };
    case "calculation/loading":
      return { ...state, isLoading: action.payload };
    case "calculation/error":
      return { ...state, error: action.payload };
    default:
      return state;
  }
}
