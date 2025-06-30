#!/usr/bin/env bash
set -euo pipefail

# This script runs inside the LocalStack container (READY stage).
# It creates the buckets listed in the S3_INITIAL_BUCKETS env var.
# If S3_INITIAL_BUCKETS is not set, it falls back to "uploads".

IFS="," read -ra BUCKETS <<< "${S3_INITIAL_BUCKETS:-uploads}"
for b in "${BUCKETS[@]}"; do
  echo "[init] creating bucket: $b"
  awslocal s3 mb "s3://$b" || true
done
