import { resolve } from 'path'
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  base: process.env.GITHUB_PAGES ? "/ACT_PingPlugin/" : "./",
  build: {
    rollupOptions: {
      input: {
        index: resolve(__dirname, 'index.html'),
        ping: resolve(__dirname, 'ping.html'),
        ping_oneline: resolve(__dirname, 'ping_oneline.html'),
      }
    }
  }
})
