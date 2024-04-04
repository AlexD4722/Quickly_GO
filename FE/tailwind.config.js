/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
    'node_modules/flowbite-react/lib/esm/**/*.js'
  ],
  theme: {
    extend: {
      screens: {
        //min-width
        sm: '280px',
        smd: '360px',
        md: '640px',
        lg: '1000px',
        xl: '1280px',
        xxl: '1580px',
        none: '0px'
      },  
    },
  },
  plugins: [
    require('flowbite/plugin')
  ]
}


