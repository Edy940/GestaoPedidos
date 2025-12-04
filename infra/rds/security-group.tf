resource "aws_security_group" "rds_sg" {
  name        = "gestaopedidos-rds-sg"
  description = "Security Group do RDS PostgreSQL"
  vpc_id      = aws_vpc.main.id

  ingress {
    description = "PostgreSQL"
    from_port   = 5432
    to_port     = 5432
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"] # depois em produção a gente restringe
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Name = "gestaopedidos-rds-sg"
  }
}
