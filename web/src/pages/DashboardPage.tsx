import { Box, Card, CardContent, Chip, Stack, Typography } from "@mui/material";
import { useHotels } from "../features/hotels/hooks/useHotels";

type StatCardProps = {
  label: string;
  value: string | number;
  hint?: string;
};

function StatCard({ label, value, hint }: StatCardProps) {
  return (
    <Card>
      <CardContent>
        <Typography variant="body2" color="text.secondary">
          {label}
        </Typography>
        <Typography variant="h4" component="p" sx={{ mt: 1 }}>
          {value}
        </Typography>
        {hint && (
          <Typography variant="caption" color="text.secondary">
            {hint}
          </Typography>
        )}
      </CardContent>
    </Card>
  );
}

export function DashboardPage() {
  const { hotels, isLoading } = useHotels({ includeInactive: false });

  return (
    <Box>
      <Stack direction="row" spacing={1} sx={{ alignItems: "center", mb: 3 }}>
        <Typography variant="h4" component="h1">
          Pulpit
        </Typography>
        <Chip label="dane przykładowe" size="small" />
      </Stack>

      <Box
        sx={{
          display: "grid",
          gridTemplateColumns: {
            xs: "1fr",
            sm: "repeat(2, 1fr)",
            md: "repeat(4, 1fr)",
          },
          gap: 2,
        }}
      >
        <StatCard
          label="Aktywne hotele"
          value={isLoading ? "…" : hotels.length}
          hint="z bazy danych"
        />
        <StatCard label="Przyjazdy dziś" value={12} />
        <StatCard label="Pokoje do posprzątania" value={7} />
        <StatCard label="Otwarte usterki" value={3} />
      </Box>
    </Box>
  );
}
