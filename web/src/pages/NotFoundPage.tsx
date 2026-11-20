import { Link } from "react-router";
import { Box, Button, Typography } from "@mui/material";

export function NotFoundPage() {
  return (
    <Box sx={{ textAlign: "center", mt: 8 }}>
      <Typography variant="h4" component="h1" gutterBottom>
        Nie znaleziono strony
      </Typography>
      <Typography color="text.secondary" sx={{ mb: 3 }}>
        Strona, której szukasz, nie istnieje albo została przeniesiona.
      </Typography>
      <Button variant="contained" component={Link} to="/">
        Wróć na pulpit
      </Button>
    </Box>
  );
}
