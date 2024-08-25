/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./*.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        inter: ['Inter', 'sans-serif'],
      },
      colors: {
        "custom-gold": {
          DEFAULT: '#DED7BE',
        },
        "custom-blue": {
          DEFAULT: '#E2EBF5',
        },
        "custom-critical": {
          DEFAULT: "#FFE0E0",
        },
        "custom-warning": {
          DEFAULT: "#FFEDDB",
        },
      },
    },
  },
  plugins: [
    function ({ addUtilities }) {
      const newUtilities = {
        ".text-shadow-custom-gold": {
          textShadow: '-1px 0 2px #795516, 0 1px 2px #795516, 1px 0 2px #795516, 0 -1px 2px #795516',
        },
        ".text-shadow-custom-blue": {
          textShadow: '-1px 0 3px #217AA2, 0 1px 3px #217AA2, 1px 0 3px #217AA2, 0 -1px 3px #217AA2',
        },
        ".text-shadow-custom-critical": {
          textShadow: '-1px 0 2px #A30D0D, 0 1px 2px #A30D0D, 1px 0 2px #A30D0D, 0 -1px 2px #A30D0D',
        },
        ".text-shadow-custom-warning": {
          textShadow: '-1px 0 2px #FF7F00, 0 1px 2px #FF7F00, 1px 0 2px #FF7F00, 0 -1px 2px #FF7F00',
        },
      };
      addUtilities(newUtilities);
    }
  ],
}
