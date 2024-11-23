interface Config {
  apiBaseUrl: string;
}

const config: Config = {
  apiBaseUrl: import.meta.env.VITE_API_BASE_URL || "http://localhost:5046/api",
};

export default config;
