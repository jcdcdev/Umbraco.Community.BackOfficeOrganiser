import { defineConfig } from "vite";

export default defineConfig({
  build: {
    lib: {
      entry: "src/backoffice-organiser.ts", // your web component source file
      formats: ["es"],
    },
    outDir: "../Umbraco.Community.BackOfficeOrganiser/wwwroot/App_Plugins/Umbraco.Community.BackOfficeOrganiser/dist/", // your web component will be saved in this location
    sourcemap: true,
    rollupOptions: {
      external: [/^@umbraco-ui/],
    },
  },
});