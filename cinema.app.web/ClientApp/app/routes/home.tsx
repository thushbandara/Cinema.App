import type { Route } from "./+types/home";
import Cinema from "../components/cinema/cinema";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "GIC Cinema" },
    { name: "description", content: "Welcome to GIC Cinema" },
  ];
}

export default function Home() {
  return <Cinema />;
}
