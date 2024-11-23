interface ResultDisplayProps {
  result: number;
}

function ResultDisplay({ result }: ResultDisplayProps) {
  return (
    <div className="mt-6 rounded-lg border border-green-200 bg-green-50 p-4">
      <div className="text-center">
        <p className="text-3xl font-bold text-green-900">{result.toFixed(4)}</p>
      </div>
    </div>
  );
}

export default ResultDisplay;
