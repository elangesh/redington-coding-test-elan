function Header() {
  return (
    <div className="mb-8 text-center">
      <div className="mb-4 flex w-full items-center justify-start">
        <a href="https://redington.co.uk/">
          <img
            src="/redington-logo-dark.png"
            width="97"
            height="57"
            alt="Redington"
            id="logo"
            data-height-percentage="60"
            data-actual-width="188"
            data-actual-height="57"
          />
        </a>
        <h2 className="mx-24 text-2xl font-bold text-gray-900">
          Probability Calculator
        </h2>
      </div>
      <p className="mt-2 text-sm text-gray-600">
        Calculate combined or either probabilities
      </p>
    </div>
  );
}

export default Header;
