using UnityEngine;

public class AIController : MonoBehaviour
{
    public void AIControll(Ball ball, float aiActivationThreshold, float movementSpeed, float minYRange, float maxYRange, ref float moveUpValue, ref float moveDownValue)
    {
        if (ball != null)
        {
            float yDifference = Mathf.Abs(ball.transform.position.y - transform.position.y);

            // If the difference in y positions is greater than the threshold
            if (yDifference > aiActivationThreshold)
            {
                // If the ball is below the AI palette
                if (ball.transform.position.y < transform.position.y)
                {
                    // Move up
                    SetMovementValues(0f, 1f, ref moveUpValue, ref moveDownValue);
                }
                // If the ball is above the AI palette
                else if (ball.transform.position.y > transform.position.y)
                {
                    // Move down
                    SetMovementValues(1f, 0f, ref moveUpValue, ref moveDownValue);
                }
                // If the ball is at the same height as the AI palette
                else
                {
                    // Don't move
                    SetMovementValues(0f, 0f, ref moveUpValue, ref moveDownValue);
                }
            }
            // If the difference in y positions is not greater than the threshold
            else
            {
                // Don't move
                SetMovementValues(0f, 0f, ref moveUpValue, ref moveDownValue);
            }

            // Execute the movement
            ExecuteMovement(movementSpeed, minYRange, maxYRange, moveUpValue, moveDownValue);
        }
    }

    private void SetMovementValues(float upValue, float downValue, ref float moveUpValue, ref float moveDownValue)
    {
        moveUpValue = upValue;
        moveDownValue = downValue;
    }

    private void ExecuteMovement(float movementSpeed, float minY, float maxY, float moveUpValue, float moveDownValue)
    {
        float yOffset = (moveUpValue - moveDownValue) * movementSpeed * Time.deltaTime;
        float newYPosition = transform.position.y + yOffset;
        newYPosition = Mathf.Clamp(newYPosition, minY, maxY);

        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}