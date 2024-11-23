import { Loader2 } from "lucide-react";

interface CalculateButtonProps {
  isLoading: boolean;
  onClick: (e: React.MouseEvent<HTMLButtonElement>) => void;
}

function CalculateButton({
  isLoading,
  onClick,
}: CalculateButtonProps): JSX.Element {
  return (
    <div className="mt-8">
      <button
        type="submit"
        onClick={onClick}
        disabled={isLoading}
        className="flex w-full items-center justify-center rounded-lg bg-blue-600 px-4 py-3 text-white transition-colors duration-200 hover:bg-blue-700 disabled:cursor-not-allowed disabled:opacity-50"
      >
        {isLoading ? (
          <>
            <Loader2 className="mr-2 animate-spin" size={20} />
            Calculating...
          </>
        ) : (
          "Calculate"
        )}
      </button>
    </div>
  );
}

export default CalculateButton;
