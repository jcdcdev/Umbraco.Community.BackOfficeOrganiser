import { defineConfig } from "vite";

export default defineConfig({
  build: {
    lib: {
      entry: "src/backoffice-organiser.ts", // your web component source file
      formats: ["es"],
    },
    outDir: "../jcdcdev.Umbraco.BackOfficeOrganiser/wwwroot/App_Plugins/jcdcdev.Umbraco.BackOfficeOrganiser/dist/", // your web component will be saved in this location
    sourcemap: true,
    rollupOptions: {
      external: [/^@umbraco-ui/],
    },
  },
});